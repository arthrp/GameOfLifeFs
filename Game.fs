module Game
    open System.Text
    open System

    let printBoard (b: bool[,]) width height : unit =
        let s = StringBuilder()
        for y in 0..height-1 do
            for x in 0..width-1 do
                s.Append(if b.[x,y] then "■" else "□") |> ignore
            s.Append(Environment.NewLine) |> ignore
        
        Console.Clear()
        printfn "%s" (s.ToString())

    let initBoard (b: bool[,]) : unit =
        b.[2,2] <- true
        b.[2,3] <- true
        b.[2,4] <- true

    let private countNeighbours (b: bool[,]) cellX cellY : int =
        let mutable count = 0
        let mutable x, y = 0, 0
        let height, width = b.GetLength 1, b.GetLength 0

        for dy in -1..1 do
            y <- (cellY + dy + height) % height
            for dx in -1..1 do
                x <- (cellX + dx + width) % width
                count <- count + (if b.[x,y] then 1 else 0)
        if b.[cellX, cellY] then count - 1 else count

    let private getNewValue (b: bool[,]) x y : bool =
        let n = countNeighbours b x y
        match b.[x,y], n with 
        | true, (2 | 3) -> true
        | false, 3 -> true
        | _ -> false

    let makeTurn (currentBoard: bool[,]) : bool[,] = 
        let height, width = currentBoard.GetLength 1, currentBoard.GetLength 0
        let newBoard = Array2D.init width height (fun x y -> getNewValue currentBoard x y)
        newBoard