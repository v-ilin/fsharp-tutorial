module Test

    open FSharp.Data
    open FSharp.Data.HttpRequestHeaders
    open DomainTypes


    let authHeader = "Bearer zrNgtYkLDPnAa5QBEGf_L7NL0uFP6b6WLkOTH7qgaCL7k_vTgy3SQVebDHrviKY5pTR-MVeTRRpNJQi3fGPep1p2xmK5rCBdGgHhrNUvg5gr71DhVFIQ4AXDfVxOmjb-0KTm_7WxINCrZCtoDbcRlIO2epE1oPeLIQCLiPC8Cy-i_-849WQQlEXkKXvvswAHxiay4wK0HyR5sjnr4BETep6DPC6vkLaqbtJDfOMuI21DUeuv3tU_l5Y10b92Ynyurv5qqx4AeaL0hVNKBvpw0I4wmcg2j055cWUI7OHVRRbunecGOCS_uJ7weqDRrkFx0-f6roGxSaLK7Bc7Hm1JWg"
    let topicsUrl = "https://firstbook.flsw.ru/api/topics"

    let GetRequestBody x:HttpResponseBody =  x.Body

    type TopicsJson = JsonProvider<"""[{"id":2,"name":"dwddwd"}]""">

    type Topic = { id:int; name:string }

    try
        let result = Http.Request
                        (topicsUrl,
                        httpMethod = "GET",
                        headers = [ Authorization authHeader ])

        match result.StatusCode with
        | 200 ->
            printfn "Request succeeded!"

            match result.Body with
            | Text text ->
                TopicsJson.Parse(text)

                |> Array.map(fun jsonValue ->
                    { id = jsonValue.Id; name = jsonValue.Name })

                |> Array.iter(fun x -> 
                    let y = x.ToString()
                    printfn "%s" y)

            | Binary binary -> printfn "Request returned response with binary content!"

        | 403 -> printfn "Forbidden!"

        | _   -> printfn "Unsupported status code was returned!"

    with
        | ex -> printfn "Sending request failed with message: \"%s\"" ex.Message
