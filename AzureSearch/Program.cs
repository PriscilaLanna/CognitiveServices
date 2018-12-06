using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureSearch
{

    class IndexLetras
    {
        [Key]
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string Id { get; set; }

        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string NomeBanda { get; set; }

        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string Album { get; set; }

        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        [IsSearchable]
        public string Letra { get; set; }

    }

    class Program
    {        
        static void Main(string[] args)
        {           
            SearchServiceClient serviceClient = new SearchServiceClient("teste-azuresearch-dois",
                new SearchCredentials("4A91F9970AB73ADB4106D6BC56FD0EE2"));

            var index = serviceClient.Indexes.GetClient("index-bandas");
            var index2 = serviceClient.Indexes.Get("index-bandas");

            //index2.Analyzers.Add(new PatternAnalyzer
            //{
            //    Name = "custom",
            //    Pattern = @"||,||"
            //});


            //index2.Analyzers.Add(new CustomAnalyzer
            //{
            //    Name = "custom",
            //    Tokenizer = TokenizerName.Standard,
            //    TokenFilters = new[] {
            //        TokenFilterName.Phonetic,
            //        TokenFilterName.Lowercase,
            //        TokenFilterName.AsciiFolding
            //    }
            //});

            //index2.Fields[3].Analyzer = "custom";

            //serviceClient.Indexes.CreateOrUpdate(index2, true);

            var batch = IndexBatch.Upload<IndexLetras>(new List<IndexLetras>
            {
               new IndexLetras(){
                   Id = "330845",
                   NomeBanda = "Ed Sheeran",
                   Album = "Não sei",
                   #region Letra
                   Letra = @"I found a love for me
Darling, just dive right in
and follow my lead
Well, I found a girl, beautiful and sweet
Oh, I never knew you were
The someone waiting for me
'Cause we were just kids
When we fell in love
Not knowing what it was
I will not give you up this time
But darling, just kiss me slow
Your heart is all I own
And in your eyes you're holding mine

Baby, I'm dancing in the dark
With you between my arms
Barefoot on the grass
Listening to our favorite song
When you said you looked a mess
I whispered underneath my breath
But you heard it, darling
You look perfect tonight

Well I found a woman, stronger
Than anyone I know
She shares my dreams
I hope that someday I'll share her home
I found a love, to carry
More than just my secrets
To carry love, to carry
children of our own
We are still kids
But we're so in love
Fighting against all odds
I know we'll be alright this time
Darling, just hold my hand
Be my girl, I'll be your man
I see my future in your eyes

Baby, I'm dancing in the dark
With you between my arms
Barefoot on the grass
Listening to our favorite song
When I saw you in that dress
Looking so beautiful
I don't deserve this, darling
You look perfect tonight

Baby, I'm dancing in the dark
With you between my arms
Barefoot on the grass
Listening to our favorite song
I have faith in what I see
Now I know I have met an angel in person
And she looks perfect, I don't deserve this
You look perfect tonight"
#endregion
               }
            });

            Console.WriteLine("Digite um termo para a busca");

            var term = Console.ReadLine();
            var result = index.Documents.Search<IndexLetras>(term, 
                new SearchParameters { IncludeTotalResultCount = true});

            Console.WriteLine($"{result.Results} resultados encontratos");

            if (result.Results.Count > 0)
            {
                foreach (var item in result.Results)
                {
                    Console.WriteLine($"{item.Document.Id} - {item.Document.NomeBanda}");
                }
            }
            else Console.WriteLine("Não encontrou nada");
            Console.Read();
        }
    }
}
