# DOCUMENTAÇÃO DE ARQUITETURA DO SISTEMA - PROJETO MERCADO D.

## 1. Conseito do Projeto

Este é um projeto usando `Clean Architecture`, utilizando blocos de construção do `Domain-Driven Design`. \
As operações de leitura e escrita foram separadas de acordo com o padrão `CQRS`. \
A observabilidade do sistema é garantida pela implementação do `OpenTelemetry` e do `Aspire Dashboard`. \
Além disso, você encontrará implementações de padrões de design como mediador, fábrica, estratégia e vários outros. 

A implementação da lógica de domínio de negócios é reduzida ao mínimo. \
Casos de uso selecionados foram implementados para demonstrar a comunicação entre camadas e o uso de eventos de domínio e integração.

## 2. Estrutura do projeto

Diferente de um projeto tradicional DDD, foi inseridada uma na camada `Cross-Cutting` que segue como espaço para 
biliotecas internas de apoio ao projeto. \
Nenhum projeto no da camada `Cross-Cutting` pode fazer referência a nenhum projeto da solução.

Também foi inserido o conseito de inversão de dependência no projeto `MercadoD.Di.Ioc` deixando mais fácil a injeção de 
dependencia para projetos futura da camada de apresentação.
Toda injeção de dependencia é feita internamete dentro de cada projeto deixando suas implementações protegidas e
e dando um comportamento de separeção de resposabilidade. \
O projeto `MercadoD.Di.Ioc` chama a implementação de cada projeto exceto a camada de apresentação realizando 
a integração completa da injeção de dependência.

A orquestração local fica a cargo do projeto `MercadoD.Host` que provisiona toda infra e configuração.

![Diagrama de Arquitetura](ArcMercadoD.png)
