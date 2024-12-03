s = [sorted(a) for a in [*zip(*[(int(x[0]),int(x[3])) for x in [l.split(' ') for l in open('in/01.txt').readlines()]])]]
print(sum([abs(x-y) for x,y in [*zip(*s)]]))
print(sum([x*(s[1].count(x)) for x in s[0]]))