inp = open("in/09.txt").read()
fp = 0
bp,br = len(inp)+1,0
s = 0
n = 0
while fp < bp:
    if fp%2 == 0:
        nn = int(inp[fp])
        s += (fp//2)*(nn+n+n-1)*nn//2
        n += nn
        fp += 1
    else:
        nn = int(inp[fp])
        while nn > 0:
            nn -= 1
            while br == 0:
                bp -= 2
                br = int(inp[bp])
            s += (bp//2) * n
            n += 1
            br -= 1
        fp += 1
while br > 0:
    s += (bp//2) * n
    n += 1
    br -= 1
print(s)

spaces= []
numeros = []
pos = 0
space = False
id=0
for a in inp:
    if space:
        spaces.append((pos,int(a)))
    else:
        numeros.append((pos,int(a),id))
        id += 1
    pos += int(a)
    space = not space
for j in range(len(numeros)-1,-1,-1):
    p,s,id = numeros[j]
    for i in range(len(spaces)):
        pp,ss = spaces[i]
        if ss >= s and pp < p:
            if ss == s:
                spaces.remove((pp,ss))
            else:
                spaces[i] = (pp+s,ss-s)
            numeros[j] = (pp,s,id)
            break
ss = 0
for p,s,id in numeros:
    ss += (id*(s+p+p-1)*s)//2
print(ss)
