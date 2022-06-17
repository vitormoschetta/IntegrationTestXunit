namespace IntegrationTest.Tests;
[Collection("IntegrationTest")]
[TestCaseOrderer("IntegrationTest.Helpers.AlphabeticalOrderer", "IntegrationTest")]
public class IntegrationTest2 : IClassFixture<ContextFixture>
{
    ContextFixture _fixture;

    public IntegrationTest2(ContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Test1()
    {
        Console.WriteLine("IntegrationTest2.Test1 | ThreadId: {0}", Thread.CurrentThread.ManagedThreadId);

        var response = await _fixture.HttpClient.GetAsync("v1/todos");
        var content = await response.Content.ReadAsStringAsync();
        var todos = JsonConvert.DeserializeObject<TodoItem[]>(content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Empty(todos);
    }

    [Fact]
    public async Task Test2()
    {
        Console.WriteLine("IntegrationTest2.Test2 | ThreadId: {0}", Thread.CurrentThread.ManagedThreadId);

        var todo = new TodoItem("Test Todo");
        var content = new StringContent(JsonConvert.SerializeObject(todo), Encoding.UTF8, "application/json");
        var response = await _fixture.HttpClient.PostAsync("v1/todos", content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Test3()
    {
        Console.WriteLine("IntegrationTest2.Test3 | ThreadId: {0}", Thread.CurrentThread.ManagedThreadId);

        var response = await _fixture.HttpClient.GetAsync("v1/todos");
        var content = await response.Content.ReadAsStringAsync();
        var todos = JsonConvert.DeserializeObject<TodoItem[]>(content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(todos);
        Assert.Equal("Test Todo", todos.First().Title);
    }


    [Fact]
    public async Task Test4()
    {
        Console.WriteLine("IntegrationTest2.Test4 | ThreadId: {0}", Thread.CurrentThread.ManagedThreadId);

        var todos = await _fixture.DbConnection.QueryAsync<TodoItem>("SELECT * FROM \"Todos\"");

        Assert.Single(todos);
        Assert.Equal("Test Todo", todos.First().Title);
    }
}