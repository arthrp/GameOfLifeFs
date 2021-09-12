open System
open Game
open System.Threading

[<EntryPoint>]
let main argv =
    let width = 50;
    let height = 20;
    let mutable board = Array2D.init width height (fun _ _ -> false)

    initBoard board

    while true do
        printBoard board width height
        Thread.Sleep(500)
        board <- makeTurn board

    0 // return an integer exit code