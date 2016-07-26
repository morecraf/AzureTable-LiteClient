## AzureTable.LiteClient - simple, complete and super fast Azure Table Storage client for .NET.

Unlike the official [Azure Storage SDK for .NET](https://github.com/Azure/azure-storage-net), AzureTable.LiteClient has the following features:
* fully support for POCO
* fully support for dynamic and anonymous objects
* PCL only (small footprint - optimal to be used with Xamarin)
* minimal dependencies (only Newtosoft.Json), no OData dependencies

You can get the binaries from [Nuget](https://www.nuget.org/packages/AzureTable.LiteClient/).

## Get table reference

```csharp

AzureTableClient tableClient = new AzureTableClient("yourAccountName", @"yourAccountKey");
AzureTable table = tableClient.GetTableReference("people");

```
## Insert an Entity

```csharp

TableResult result = await table.InsertAsync(new {PartitionKey="Harp",RowKey="jd_01",FirstName="John", LastName="Doe" });

```

or insert using a custom entity:

```csharp
 public class Person
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
Person p = new Person {PartitionKey="Harp",RowKey="jd_01",FirstName="John", LastName="Doe" };

TableResult result = await table.InsertAsync(p);

```
Note: every entity should have the PartitionKey and the RowKey properties, this is the only mandatory requirement.


## Get the entity by PK and RK:
```csharp
dynamic entity = await table.FindOneAsync("Harp","jd_01");

//or map it to your POCO
Person p = await table.FindOneAsync<Person>("Harp","jd_01");
```

## Add new property value and keep existing properties and values:

```java
dynamic entity = await table.FindOneAsync("Harp","jd_01");
entity.Email = "cristi@contoso.com";
var result=await table.MergeAsync(item);


```
Note: Azure Table Storage entities may have a dynamic number of properties. So one entity from a table may have 2 properties and other may have 3 (or more or less).

## Query by other fields:

```csharp

  IEnumerable<dynamic> allPersons = await table.FindAsync(filter: "FirstName eq 'John'");
       
 // or map it to custom POCOs
 IEnumerable<Person> allPersons = await table.FindAsync<Person>(filter: "FirstName eq 'John'");
                   
          
```

examples of other types or more complex queries:

```csharp

//get all entities for WHERE PartitionKey="Harp" AND Age>30
string myFilter="PartitionKey eq 'Harp'" and Age gt 30"
var items = await table.FindAsync<Person>(filter:myFilter);

//get by timestamp (WHERE Timestamp < now)

string myFilter = string.Format("Timestamp lt datetime'{0}'", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));

var items = await table.FindAsync<Person>(filter:myFilter);

```

The query syntax is based on the REST API, more info about the syntax can be found [here](https://msdn.microsoft.com/en-us/library/azure/dd894031.aspx). The filter string should NOT be URL encoded, the library encodes it for you.


## FAQ

Q: Who is using it?
A: [Siaqodb](http://siaqodb.com) will use it very soon, stay tuned!




