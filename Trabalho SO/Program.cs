using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

class BankerAlgorithm
{
    // Definições do sistema conforme o enunciado
    private const int NUMBER_OF_CUSTOMERS = 5; // 
    private const int NUMBER_OF_RESOURCES = 3; // 

    // Estruturas de dados 
    private static int[] available = new int[NUMBER_OF_RESOURCES];
    private static int[,] maximum = new int[NUMBER_OF_CUSTOMERS, NUMBER_OF_RESOURCES];
    private static int[,] allocation = new int[NUMBER_OF_CUSTOMERS, NUMBER_OF_RESOURCES];
    private static int[,] need = new int[NUMBER_OF_CUSTOMERS, NUMBER_OF_RESOURCES];

    private static readonly object _bankerLock = new object();
    private static readonly Random _random = new Random();

    static async Task Main(string[] args)
    {
        // Valida entrada via linha de comando 
        if (args.Length != NUMBER_OF_RESOURCES)
        {
            Console.WriteLine($"Erro: Informe {NUMBER_OF_RESOURCES} valores de recursos.");
            return;
        }

        for (int i = 0; i < NUMBER_OF_RESOURCES; i++)
        {
            available[i] = int.Parse(args[i]); // 
        }

        // Inicializa matrizes de demanda máxima e necessidade 
        for (int i = 0; i < NUMBER_OF_CUSTOMERS; i++)
        {
            for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
            {
                maximum[i, j] = _random.Next(1, available[j] + 1);
                need[i, j] = maximum[i, j];
                allocation[i, j] = 0;
            }
        }

        Console.WriteLine("=== Simulador do Algoritmo do Banqueiro iniciado ===");
        
        // Criação das threads (clientes) 
        Task[] customers = new Task[NUMBER_OF_CUSTOMERS];
        for (int i = 0; i < NUMBER_OF_CUSTOMERS; i++)
        {
            int customerId = i;
            customers[i] = Task.Run(() => CustomerBehavior(customerId));
        }

        await Task.WhenAll(customers);
    }

    // Comportamento do cliente: loop contínuo de solicitar e liberar 
    private static void CustomerBehavior(int customerId)
    {
        while (true)
        {
            int[] request = new int[NUMBER_OF_RESOURCES];
            
            lock (_bankerLock)
            {
                for (int i = 0; i < NUMBER_OF_RESOURCES; i++)
                {
                    request[i] = _random.Next(0, need[customerId, i] + 1); // 
                }
            }

            if (RequestResources(customerId, request) == 0)
            {
                Thread.Sleep(_random.Next(1000, 3000)); // Simula uso dos recursos
                ReleaseResources(customerId, request);
                Thread.Sleep(_random.Next(1000, 2000)); // Tempo antes da próxima requisição
            }
        }
    }

    // Implementação da solicitação 
    private static int RequestResources(int customerNum, int[] request)
    {
        lock (_bankerLock) // Mutex para prevenir condições de corrida 
        {
            Console.WriteLine($"\nCliente {customerNum} solicita: {string.Join(", ", request)}");

            // Verifica se a solicitação é maior que a necessidade ou disponibilidade imediata
            for (int i = 0; i < NUMBER_OF_RESOURCES; i++)
            {
                if (request[i] > need[customerNum, i] || request[i] > available[i])
                {
                    Console.WriteLine($"   [NEGADA] Recursos insuficientes no momento.");
                    return -1;
                }
            }

            // Simulação de alocação (Estado Provisório)
            for (int i = 0; i < NUMBER_OF_RESOURCES; i++)
            {
                available[i] -= request[i];
                allocation[customerNum, i] += request[i];
                need[customerNum, i] -= request[i];
            }

            // Verifica se o estado resultante é seguro 
            if (IsSafeState())
            {
                Console.WriteLine($"   [APROVADA] Sistema permanece em estado seguro.");
                return 0;
            }
            else
            {
                // Rollback: desfaz a alocação se for inseguro 
                for (int i = 0; i < NUMBER_OF_RESOURCES; i++)
                {
                    available[i] += request[i];
                    allocation[customerNum, i] -= request[i];
                    need[customerNum, i] += request[i];
                }
                Console.WriteLine($"   [NEGADA] Estado resultante seria inseguro.");
                return -1;
            }
        }
    }

    // Implementação da liberação 
    private static int ReleaseResources(int customerNum, int[] release)
    {
        lock (_bankerLock)
        {
            Console.WriteLine($"Cliente {customerNum} liberando: {string.Join(", ", release)}");
            for (int i = 0; i < NUMBER_OF_RESOURCES; i++)
            {
                available[i] += release[i];
                allocation[customerNum, i] -= release[i];
                need[customerNum, i] += release[i];
            }
            return 0;
        }
    }

    // Algoritmo de Segurança (Seção 7.5.3.1) 
    private static bool IsSafeState()
    {
        int[] work = (int[])available.Clone();
        bool[] finish = new bool[NUMBER_OF_CUSTOMERS];

        for (int count = 0; count < NUMBER_OF_CUSTOMERS; count++)
        {
            bool found = false;
            for (int i = 0; i < NUMBER_OF_CUSTOMERS; i++)
            {
                if (!finish[i])
                {
                    bool canAllocate = true;
                    for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
                    {
                        if (need[i, j] > work[j])
                        {
                            canAllocate = false;
                            break;
                        }
                    }

                    if (canAllocate)
                    {
                        for (int k = 0; k < NUMBER_OF_RESOURCES; k++)
                            work[k] += allocation[i, k];
                        
                        finish[i] = true;
                        found = true;
                    }
                }
            }
            if (!found) break;
        }

        return finish.All(f => f);
    }
}