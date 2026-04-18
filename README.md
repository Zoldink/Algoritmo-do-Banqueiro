# Algoritmo do Banqueiro - Simulação de Gerenciamento de Recursos
Este repositório contém a implementação do Algoritmo do Banqueiro, desenvolvida para a disciplina de Sistemas Operacionais na PUC Minas - Campus Betim.
O projeto foca na prevenção de deadlocks em sistemas multithreaded.

## 📌 Visão Geral
O programa simula o funcionamento de um sistema operacional que gerencia a alocação de recursos entre diversos processos (clientes). O objetivo é garantir que o sistema nunca entre em um estado inseguro, onde um impasse (deadlock) seria inevitável.

3Principais Funcionalidades:
Controle de Recursos: Gerenciamento de múltiplos tipos de recursos.

Multithreading: Simulação de clientes através de threads independentes.

Segurança: Verificação de estado seguro antes de qualquer alocação definitiva.

Exclusão Mútua: Proteção de estruturas de dados compartilhadas para evitar condições de corrida.

## 🛠️ Tecnologias e Conceitos
Linguagem: C# (.NET 10.0)

Concorrência: Utilização de Task para simular o comportamento dos processos.

Sincronização: Uso de lock (monitores) para garantir a integridade dos dados.

Algoritmo: Implementação baseada na Seção 7.5.3 do livro Fundamentos de Sistemas Operacionais (Silberschatz).

## 🚀 Como Compilar e Executar
Pré-requisitos
.NET SDK instalado na máquina.

Execução
Clone este repositório.

Abra o terminal na pasta do projeto.

Execute o comando passando a quantidade de instâncias disponíveis para cada recurso (exemplo para 3 recursos):

Bash
dotnet run -- 10 5 7

Aluno: Eric Gomes Cordeiro - 882760
Curso: Sistemas de Informação - 3º Período
