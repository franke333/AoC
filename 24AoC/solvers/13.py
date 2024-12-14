inp =[tuple([*[int(k[a].split('+')[b][:2]) for a,b in [(0,1),(0,2),(1,1),(1,2)]],int(k[2].split('=')[1].split(',')[0]),int(k[2].split('=')[2])]) \
      for k in [r.split('\n') for r in open("in/13.txt").read().split('\n\n')]]
lb = lambda a,b,c,d,X,Y: (X*b-Y*a)/(b*c-d*a)
la = lambda a,b,c,d,X,Y: (Y-lb(b,a,d,c,Y,X)*d)/b
t = lambda inp: [int(3*A+B) for a,b,c,d,X,Y in inp if ((A := la(a,b,c,d,X,Y)) >= 0 and (B := lb(a,b,c,d,X,Y)) >= 0 and (A%1 == 0) and (B%1 == 0))]
print(sum(t(inp)))
print(sum(t(((ax,ay,bx,by,X+10000000000000,Y+10000000000000) for ax,ay,bx,by,X,Y in inp))))