# Checkpoint01

## Descrição

Este projeto implementa um sistema de gerenciamento de sessões de usuário para uma aplicação web de comércio eletrônico. O sistema utiliza **MySQL** para armazenar os dados do usuário e **Redis** para cache de sessão ativa, garantindo respostas rápidas e redução de acessos repetidos ao banco de dados.

---

## Estrutura de Classes

* **Usuario**: representa o perfil do usuário, com as propriedades:

  * `Id`: identificador único do usuário.
  * `Nome`: nome completo do usuário.
  * `Email`: e-mail do usuário.
  * `UltimoAcesso`: data do último acesso, utilizada para monitoramento e cache.

Essa classe é usada tanto para mapear os dados retornados do MySQL quanto para armazenar os dados no Redis.

---

## Conexões com MySQL e Redis

* **MySQL**: a conexão é gerenciada por um serviço dedicado (`MySqlService`), que encapsula a lógica de acesso ao banco e fornece métodos assíncronos para buscar usuários por `Id`. Isso facilita testes, manutenção e garante desempenho em consultas.

* **Redis**: o cache é manipulado através de um serviço (`RedisService`) que centraliza a conexão, usando o padrão de inicialização preguiçosa (Lazy) para criar a conexão apenas quando necessário. O serviço oferece métodos para ler e gravar dados serializados em JSON, com tempo de expiração configurável (15 minutos), garantindo dados atualizados e economizando recursos do banco.

---

## Lógica de Cache e Fallback

1. **Verificação de Cache**: ao buscar os dados de um usuário, o sistema primeiro consulta o Redis. Se os dados estiverem disponíveis, são retornados imediatamente, garantindo baixa latência.
2. **Fallback para MySQL**: caso os dados não estejam no cache, o sistema consulta o MySQL utilizando o `MySqlService`.
3. **Atualização do Cache**: os dados obtidos do MySQL são serializados em JSON e armazenados no Redis com expiração de 15 minutos. Isso evita acessos repetidos ao banco e melhora a performance.

---

## Boas Práticas Implementadas

* Uso de **async/await** para chamadas assíncronas, evitando bloqueio de threads.
* Separação clara de responsabilidades entre **persistência (MySQL)**, **cache (Redis)** e **lógica de negócio (serviço principal)**.
* Tratamento de **exceções** para conexões e serialização, garantindo robustez da aplicação.
* Configuração de **tempo de expiração no Redis**, evitando dados desatualizados e controlando o uso de memória.

----

## Resumo

O sistema de gerenciamento de sessões de usuário em C# utiliza uma classe Usuario para representar o perfil, com Id, Nome, Email e UltimoAcesso, sendo usada tanto para mapear os dados do MySQL quanto para armazená-los no Redis. A conexão com o MySQL é feita por meio de um serviço dedicado que centraliza consultas assíncronas, enquanto o Redis é acessado por um serviço que gerencia a conexão e armazena os dados em JSON com expiração de 15 minutos. Ao buscar um usuário, o sistema primeiro verifica se os dados estão no cache Redis e, se não estiverem, consulta o MySQL, atualizando o cache em seguida. A implementação adota boas práticas como uso de chamadas assíncronas, separação de responsabilidades, tratamento de exceções e configuração de expiração no Redis, garantindo que o sistema seja performático, escalável e fácil de manter.
