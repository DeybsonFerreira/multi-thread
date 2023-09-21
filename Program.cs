using multi_thread;
using multi_thread.Models;

List<string> listCep = new()
{
    "13178574",
    "13179190",
    "13179180",
    "13010080",
    "13026909"
};

await MultiThread(listCep);
await NoMultiThread(listCep);



//Processo com MultiThread
static async Task MultiThread(List<string> listCep)
{
    Console.WriteLine("++++ Iniciando processo com MultiThread ++++");
    BuscarCepHttp http = new();
    List<Task<Endereco>> list = new();

    //processo 1
    foreach (var cep in listCep)
    {
        Task<Endereco> addressTask = http.FindAddress(cep);
        list.Add(addressTask);
    }

    //processo 2
    await Task.WhenAll(list);

    List<Endereco> allResult = list.Select(c => c.Result).ToList();

    //processo 3
    foreach (var item in allResult)
        Console.WriteLine(item.Logradouro);

}


//Processo sem Multi-Thread
static async Task NoMultiThread(List<string> listCep)
{
    Console.WriteLine("++++ Iniciando processo sem MultiThread ++++");
    BuscarCepHttp http = new();
    List<Endereco> list = new();

    //processo 1
    foreach (var cep in listCep)
    {
        Endereco address = await http.FindAddress(cep);
        list.Add(address);
    }

    //processo 2
    foreach (var item in list)
        Console.WriteLine(item.Logradouro);

}

