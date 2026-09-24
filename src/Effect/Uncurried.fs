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
let mkEffectFn5 = box (fun (f: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> 
    let f1 = sharpurs_apply f a1
    let f2 = sharpurs_apply f1 a2
    let f3 = sharpurs_apply f2 a3
    let f4 = sharpurs_apply f3 a4
    let f5 = sharpurs_apply f4 a5
    sharpurs_apply f5 null
))))))
let mkEffectFn6 = box (fun (f: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> 
    let f1 = sharpurs_apply f a1
    let f2 = sharpurs_apply f1 a2
    let f3 = sharpurs_apply f2 a3
    let f4 = sharpurs_apply f3 a4
    let f5 = sharpurs_apply f4 a5
    let f6 = sharpurs_apply f5 a6
    sharpurs_apply f6 null
)))))))
let mkEffectFn7 = box (fun (f: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> box (fun (a7: obj) -> 
    let f1 = sharpurs_apply f a1
    let f2 = sharpurs_apply f1 a2
    let f3 = sharpurs_apply f2 a3
    let f4 = sharpurs_apply f3 a4
    let f5 = sharpurs_apply f4 a5
    let f6 = sharpurs_apply f5 a6
    let f7 = sharpurs_apply f6 a7
    sharpurs_apply f7 null
))))))))
let mkEffectFn8 = box (fun (f: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> box (fun (a7: obj) -> box (fun (a8: obj) -> 
    let f1 = sharpurs_apply f a1
    let f2 = sharpurs_apply f1 a2
    let f3 = sharpurs_apply f2 a3
    let f4 = sharpurs_apply f3 a4
    let f5 = sharpurs_apply f4 a5
    let f6 = sharpurs_apply f5 a6
    let f7 = sharpurs_apply f6 a7
    let f8 = sharpurs_apply f7 a8
    sharpurs_apply f8 null
)))))))))
let mkEffectFn9 = box (fun (f: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> box (fun (a7: obj) -> box (fun (a8: obj) -> box (fun (a9: obj) -> 
    let f1 = sharpurs_apply f a1
    let f2 = sharpurs_apply f1 a2
    let f3 = sharpurs_apply f2 a3
    let f4 = sharpurs_apply f3 a4
    let f5 = sharpurs_apply f4 a5
    let f6 = sharpurs_apply f5 a6
    let f7 = sharpurs_apply f6 a7
    let f8 = sharpurs_apply f7 a8
    let f9 = sharpurs_apply f8 a9
    sharpurs_apply f9 null
))))))))))
let mkEffectFn10 = box (fun (f: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> box (fun (a7: obj) -> box (fun (a8: obj) -> box (fun (a9: obj) -> box (fun (a10: obj) -> 
    let f1 = sharpurs_apply f a1
    let f2 = sharpurs_apply f1 a2
    let f3 = sharpurs_apply f2 a3
    let f4 = sharpurs_apply f3 a4
    let f5 = sharpurs_apply f4 a5
    let f6 = sharpurs_apply f5 a6
    let f7 = sharpurs_apply f6 a7
    let f8 = sharpurs_apply f7 a8
    let f9 = sharpurs_apply f8 a9
    let f10 = sharpurs_apply f9 a10
    sharpurs_apply f10 null
)))))))))))

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

let runEffectFn5 = box (fun (eff: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun _ ->
    let e1 = eff :?> (obj -> obj)
    let e2 = e1 a1 :?> (obj -> obj)
    let e3 = e2 a2 :?> (obj -> obj)
    let e4 = e3 a3 :?> (obj -> obj)
    let e5 = e4 a4 :?> (obj -> obj)
    e5 a5
)
))))))
let runEffectFn6 = box (fun (eff: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> box (fun _ ->
    let e1 = eff :?> (obj -> obj)
    let e2 = e1 a1 :?> (obj -> obj)
    let e3 = e2 a2 :?> (obj -> obj)
    let e4 = e3 a3 :?> (obj -> obj)
    let e5 = e4 a4 :?> (obj -> obj)
    let e6 = e5 a5 :?> (obj -> obj)
    e6 a6
)
)))))))
let runEffectFn7 = box (fun (eff: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> box (fun (a7: obj) -> box (fun _ ->
    let e1 = eff :?> (obj -> obj)
    let e2 = e1 a1 :?> (obj -> obj)
    let e3 = e2 a2 :?> (obj -> obj)
    let e4 = e3 a3 :?> (obj -> obj)
    let e5 = e4 a4 :?> (obj -> obj)
    let e6 = e5 a5 :?> (obj -> obj)
    let e7 = e6 a6 :?> (obj -> obj)
    e7 a7
)
))))))))
let runEffectFn8 = box (fun (eff: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> box (fun (a7: obj) -> box (fun (a8: obj) -> box (fun _ ->
    let e1 = eff :?> (obj -> obj)
    let e2 = e1 a1 :?> (obj -> obj)
    let e3 = e2 a2 :?> (obj -> obj)
    let e4 = e3 a3 :?> (obj -> obj)
    let e5 = e4 a4 :?> (obj -> obj)
    let e6 = e5 a5 :?> (obj -> obj)
    let e7 = e6 a6 :?> (obj -> obj)
    let e8 = e7 a7 :?> (obj -> obj)
    e8 a8
)
)))))))))
let runEffectFn9 = box (fun (eff: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> box (fun (a7: obj) -> box (fun (a8: obj) -> box (fun (a9: obj) -> box (fun _ ->
    let e1 = eff :?> (obj -> obj)
    let e2 = e1 a1 :?> (obj -> obj)
    let e3 = e2 a2 :?> (obj -> obj)
    let e4 = e3 a3 :?> (obj -> obj)
    let e5 = e4 a4 :?> (obj -> obj)
    let e6 = e5 a5 :?> (obj -> obj)
    let e7 = e6 a6 :?> (obj -> obj)
    let e8 = e7 a7 :?> (obj -> obj)
    let e9 = e8 a8 :?> (obj -> obj)
    e9 a9
)
))))))))))
let runEffectFn10 = box (fun (eff: obj) -> box (fun (a1: obj) -> box (fun (a2: obj) -> box (fun (a3: obj) -> box (fun (a4: obj) -> box (fun (a5: obj) -> box (fun (a6: obj) -> box (fun (a7: obj) -> box (fun (a8: obj) -> box (fun (a9: obj) -> box (fun (a10: obj) -> box (fun _ ->
    let e1 = eff :?> (obj -> obj)
    let e2 = e1 a1 :?> (obj -> obj)
    let e3 = e2 a2 :?> (obj -> obj)
    let e4 = e3 a3 :?> (obj -> obj)
    let e5 = e4 a4 :?> (obj -> obj)
    let e6 = e5 a5 :?> (obj -> obj)
    let e7 = e6 a6 :?> (obj -> obj)
    let e8 = e7 a7 :?> (obj -> obj)
    let e9 = e8 a8 :?> (obj -> obj)
    let e10 = e9 a9 :?> (obj -> obj)
    e10 a10
)
)))))))))))
