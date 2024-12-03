inp=[[int(a) for a in l.split(' ')] for l in open("in/02.txt").readlines()]
f=lambda x:[all(0<abs(x-y)<4 for x,y in d) and (all(x<y for x,y in d) or all(x>y for x,y in d)) for d in x]
t=lambda i:[[*zip(x[:-1],x[1:])] for x in i]
print(f(t(inp)).count(1))
print([any(f(z)) for z in [t(y) for y in [[i[:j]+i[j+1:] for j in range(len(i))] for i in inp]]].count(1))