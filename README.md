## Algoritmo do Banqueiro - Simulação de Gerenciamento de Recursos
Este repositório contém a implementação do Algoritmo do Banqueiro, desenvolvida para a disciplina de Sistemas Operacionais na PUC Minas - Campus Betim.
O projeto foca na prevenção de deadlocks em sistemas multithreaded.

# 📌 Visão Geral
O programa simula o funcionamento de um sistema operacional que gerencia a alocação de recursos entre diversos processos (clientes). O objetivo é garantir que o sistema nunca entre em um estado inseguro, onde um impasse (deadlock) seria inevitável.

3Principais Funcionalidades:
Controle de Recursos: Gerenciamento de múltiplos tipos de recursos.

Multithreading: Simulação de clientes através de threads independentes.

Segurança: Verificação de estado seguro antes de qualquer alocação definitiva.

Exclusão Mútua: Proteção de estruturas de dados compartilhadas para evitar condições de corrida.

# 🛠️ Tecnologias e Conceitos
Linguagem: C# (.NET 8.0)

Concorrência: Utilização de Task para simular o comportamento dos processos.

Sincronização: Uso de lock (monitores) para garantir a integridade dos dados.

Algoritmo: Implementação baseada na Seção 7.5.3 do livro Fundamentos de Sistemas Operacionais (Silberschatz).

# 🚀 Como Compilar e Executar
Pré-requisitos
.NET SDK instalado na máquina.

Execução
Clone este repositório.

Abra o terminal na pasta do projeto.

Execute o comando passando a quantidade de instâncias disponíveis para cada recurso (exemplo para 3 recursos):

Bash
dotnet run -- 10 5 7


## 📝 Relatório de Implementação
# Introdução
O Algoritmo do Banqueiro é uma técnica de prevenção de deadlocks. Ele trabalha simulando a alocação de recursos e verificando se, após essa alocação, ainda existe uma sequência segura onde todos os processos conseguem terminar as suas tarefas.

# Desenvolvimento
A implementação foi feita em C#, estruturada da seguinte forma:

Estruturas de Dados: Matrizes para Available, Maximum, Allocation e Need.

Threads de Clientes: Cada cliente solicita recursos aleatórios em intervalos irregulares.

Segurança: A função de verificação percorre os estados possíveis para garantir que a solicitação não causará um travamento no sistema.

# Resultados
O simulador exibe em tempo real no console:

As tentativas de solicitação de cada cliente.

A decisão do "Banqueiro" (Aprovada ou Negada).

A liberação de recursos após o uso, permitindo que novos processos avancem.

# Conclusão
O projeto demonstra a importância da sincronização em sistemas concorrentes. O uso de mecanismos de trava em C# garantiu que o estado do banco permanecesse consistente, enquanto o algoritmo preveniu com sucesso qualquer situação de deadlock durante os testes.


Aluno: Eric Gomes Cordeiro - 882760
Curso: Sistemas de Informação - 3º Período
