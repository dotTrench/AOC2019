module AOC2019.Day1

open System.IO

let calculateFuel mass =
    mass / 3 - 2

let calculateTotalFuel mass =
    let rec calcFuel mass acc =
        if mass <= 8 then
            acc
        else
            let sum = calculateFuel mass
            calcFuel sum (acc + sum)

    calcFuel mass 0

let input =
    File.ReadAllLines "input.txt"
    |> Array.map int

let task1 input =
    input
    |> Seq.sumBy calculateFuel

let task2 input =
    input
    |> Seq.sumBy calculateTotalFuel

[<EntryPoint>]
let main argv =
    task1 input |> printfn "Task1: %d"
    task2 input |> printfn "Task2: %d"
    0

