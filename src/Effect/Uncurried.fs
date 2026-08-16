let mkEffectFn1 = box (fun (f: obj) -> box (fun (a: obj) ->
    let res = sharpurs_apply f a
    sharpurs_apply res null
))

let mkEffectFn2 = box (fun (f: obj) -> box (fun (a: obj) -> box (fun (b: obj) ->
    let fa = sharpurs_apply f a
    let fab = sharpurs_apply fa b
    sharpurs_apply fab null
)))

let mkEffectFn3 = box (fun (f: obj) -> box (fun (a: obj) -> box (fun (b: obj) -> box (fun (c: obj) ->
    let fa = sharpurs_apply f a
    let fab = sharpurs_apply fa b
    let fabc = sharpurs_apply fab c
    sharpurs_apply fabc null
))))

let mkEffectFn4 = box (fun (f: obj) -> box (fun (a: obj) -> box (fun (b: obj) -> box (fun (c: obj) -> box (fun (d: obj) ->
    let fa = sharpurs_apply f a
    let fab = sharpurs_apply fa b
    let fabc = sharpurs_apply fab c
    let fabcd = sharpurs_apply fabc d
    sharpurs_apply fabcd null
)))))
let mkEffectFn5 _ = undefined
let mkEffectFn6 _ = undefined
let mkEffectFn7 _ = undefined
let mkEffectFn8 _ = undefined
let mkEffectFn9 _ = undefined
let mkEffectFn10 _ = undefined

let runEffectFn1 = box (fun (eff: obj) -> box (fun (a: obj) -> box (fun _ ->
    let effT = eff :?> (obj -> obj)
    effT a
)))

let runEffectFn2 = box (fun (eff: obj) -> box (fun (a: obj) -> box (fun (b: obj) -> box (fun _ ->
    try
        let effT = eff :?> (obj -> (obj -> obj))
        effT a b
    with
    | :? System.InvalidCastException ->
        // The eff is a .NET function that takes 2 arguments or we need to apply manually.
        // But since it's an EffectFn2 from our FFI, it's box (fun a -> box (fun b -> ...))
        // So we CAN cast it to (obj -> obj) to get the inner function!
        let eff1 = eff :?> (obj -> obj)
        let eff2 = eff1 a :?> (obj -> obj)
        eff2 b
))))

let runEffectFn3 = box (fun (eff: obj) -> box (fun (a: obj) -> box (fun (b: obj) -> box (fun (c: obj) -> box (fun _ ->
    let eff1 = eff :?> (obj -> obj)
    let eff2 = eff1 a :?> (obj -> obj)
    let eff3 = eff2 b :?> (obj -> obj)
    eff3 c
)))))

let runEffectFn4 = box (fun (eff: obj) -> box (fun (a: obj) -> box (fun (b: obj) -> box (fun (c: obj) -> box (fun (d: obj) -> box (fun _ ->
    let eff1 = eff :?> (obj -> obj)
    let eff2 = eff1 a :?> (obj -> obj)
    let eff3 = eff2 b :?> (obj -> obj)
    let eff4 = eff3 c :?> (obj -> obj)
    eff4 d
))))))

let runEffectFn5 _ = undefined
let runEffectFn6 _ = undefined
let runEffectFn7 _ = undefined
let runEffectFn8 _ = undefined
let runEffectFn9 _ = undefined
let runEffectFn10 _ = undefined
