open System

type Result = 
    | Ok of char list 
    | Err of string

let rec parse acc = function
    | '.' :: rest -> parse ('0'::acc)  rest
    | '-' :: '.' :: rest -> parse ('1'::acc)  rest
    | '-' :: '-' :: rest -> parse ('2'::acc)  rest
    | [] -> Ok( List.rev acc )
    | x -> Err <| string(x)
    
let process' (s:string) = 
    s.ToCharArray() 
    |> Array.toList 
    |> parse []
    |> function
        | Ok xs -> printfn "%A - расшифровано : %s" s (new String( List.toArray xs) )
        | Err er -> printfn "%A - не удалось распознать %A" s er

[<EntryPoint>]
do
    process' ".-.--"
    process' "--."
    process' "-..-.--"
    while true do
        printf "Введите строку > " 
        let s = Console.ReadLine().Trim()
        if s = "q" then exit(0)
        process' s

        

    
