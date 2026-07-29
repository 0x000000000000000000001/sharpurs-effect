let pureE = box (fun (a: obj) -> box (fun _ -> a))
let bindE = box (fun (a: obj) -> box (fun (f: obj) -> box (fun _ -> let a_res = sharpurs_apply a undefined in let f_res = sharpurs_apply f a_res in sharpurs_apply f_res undefined)))

let untilE = box (fun (f: obj) -> box (fun _ ->
    let mutable condition = false
    while not condition do
        condition <- unbox<bool> (sharpurs_apply f undefined)
    undefined
))

let whileE = box (fun (f: obj) -> box (fun (a: obj) -> box (fun _ ->
    let mutable condition = unbox<bool> (sharpurs_apply f undefined)
    while condition do
        sharpurs_apply a undefined |> ignore
        condition <- unbox<bool> (sharpurs_apply f undefined)
    undefined
)))

let forE = box (fun (lo: obj) -> box (fun (hi: obj) -> box (fun (f: obj) -> box (fun _ ->
    let l = unbox<int> lo
    let h = unbox<int> hi
    for i = l to h - 1 do
        sharpurs_apply (sharpurs_apply f (box i)) undefined |> ignore
    undefined
))))

let foreachE = box (fun (arr: obj) -> box (fun (f: obj) -> box (fun _ ->
    let arr' = unbox<obj[]> arr
    for v in arr' do
        sharpurs_apply (sharpurs_apply f v) undefined |> ignore
    undefined
)))
