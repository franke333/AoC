priority = lambda n : ord(n) - 97 if ord(n) > 96 else ord(n) - 39
summ,summ2 = 0,0
for i,l in enumerate(open("inputs/input03.txt")):
    t = [0]*52
    if i%3 == 0:
        t2 = [0]*52
    #part 1
    for item in [priority(x) for x in l[:len(l)//2]]:
        t[item]=1
    for item in [priority(x) for x in l[len(l)//2:-1]]:
        if t[item] == 1:
            summ += item + 1
            break
    #part 2
    for item in [priority(x) for x in l[:-1]]:
        if t2[item] == i%3:
            t2[item] += 1
            if t2[item] == 3:
                summ2 += item + 1
print(summ,summ2)