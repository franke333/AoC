inp = [int(x) for x in open("in/11.txt").read().split(" ")]
blink = lambda x: [1] if x == 0 else ([int(y[len(y)//2:]), int(y[:len(y)//2])] if len(y := str(x))%2 == 0 else [x*2024])
t = {}
def hrajuSiSKamenim(kamen, stepsRemaining):
    if stepsRemaining == 0: return 1
    if (kamen, stepsRemaining) in t: return t[(kamen,stepsRemaining)]
    a = sum([hrajuSiSKamenim(x, stepsRemaining-1) for x in blink(kamen)])
    if kamen < 1000:
        t[(kamen,stepsRemaining)] = a
    return a
print(sum([hrajuSiSKamenim(x, 25) for x in inp]))
print(sum([hrajuSiSKamenim(x, 75) for x in inp]))