module FSharpImp.Tests

open Graphs.WeightedGraph
open Graphs.MinSpanningTrees

let sampleGraph = 
    (createGraph 7) 
      |> withEdge 'A' 5.0 'B' |> withEdge 'A' 12.0 'E' |> withEdge 'A' 7 'D'
      |> withEdge 'B' 7.0 'C' |> withEdge 'B' 9.0 'D'
      |> withEdge 'C' 4.0 'D' |> withEdge 'C' 2.0 'F' |> withEdge 'C' 5.0 'G'
      |> withEdge 'D' 3.0 'F' |> withEdge 'D' 4.0 'E'
      |> withEdge 'E' 7.0 'F' 
      |> withEdge 'F' 2.0 'G'

let TestKruskal () =        
    sampleGraph 
      |> kruskal 
      |> Seq.map (fun (x,y) -> sprintf "edge (%c,%c) in MST\n" ((char)x + 'A') ((char)y + 'A'))
      |> Seq.fold (fun s a -> s + a) ""
      |> printf "%s"

 
let TestPrim () = sampleGraph |> prim 0 |> printf "%A"