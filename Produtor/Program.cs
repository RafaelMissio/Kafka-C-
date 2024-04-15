using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;


var schemaConfig = new SchemaRegistryConfig{
    Url = "http://localhost:8081"
};

var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig);

var config = new ProducerConfig{BootstrapServers = "localhost:9092"};

using var producer = new ProducerBuilder<string,missio.Curso>(config)
.SetValueSerializer(new AvroSerializer<missio.Curso>(schemaRegistry))
.Build();

var message = new Message<string, missio.Curso>
{
    Key = Guid.NewGuid().ToString(),
    Value = new missio.Curso{
        Id = Guid.NewGuid().ToString(),
        Descricao = "Kafka Teste"
    }

};



var result = await producer.ProduceAsync("cursos", message);


Console.WriteLine($"{result.Offset}");