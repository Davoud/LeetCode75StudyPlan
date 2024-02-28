module FSharpImp.Graphs.MinSpanningTrees
 
 open UnionFind
 open WeightedGraph

    let prim (start: int) (graph: Graph) = 
        let max = len graph
        let intree = Array.create max false 
        let distance = Array.create max System.Double.MaxValue
        let parent = Array.create max -1
        let mutable v = start
        let mutable dist = System.Double.MaxValue

        distance[start] <- 0
    
        while not intree[v] do
            intree[v] <- true
            for (y, weight) in graph |> neighborsOf v do            
                if distance[y] > weight && not intree[y] then
                    distance[y] <- weight
                    parent[y] <- v
            v <- 0
            dist <- System.Double.MaxValue
            for i in 0 .. max - 1 do
                if not intree[i] && dist > distance[i] then
                    dist <- distance[i]
                    v <- i
        parent

     let kruskal (g: Graph) = seq {
        let s = newSetUnion (len g)                                 
        let allEdgesSorted = 
            g |> Seq.mapi (fun x edges -> edges |> Seq.map (fun (y, w) -> x, y, w)) 
                |> Seq.concat 
                |> Seq.sortBy (fun (_,_,w) -> w)       
        for x, y, _ in allEdgesSorted do                
            if not (inSameComponent s x y) then           
                yield x,y       
                union s x y 
     }

     


