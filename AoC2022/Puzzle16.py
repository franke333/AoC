from itertools import combinations,permutations
from math import ceil
class Node:
    nodes = {}
    def __init__(self, flow, name, neighs):
        self.name = name
        self.flow = flow
        self.neighs = {} 
        for n in neighs:
            self.neighs[n] = 1
        Node.nodes[name] = self
    def reduce(self):
        for node,cost in self.neighs.items():
            for node2,cost2 in self.neighs.items():
                if node != node2:
                    nodeObj = Node.nodes[node]
                    nodeObj.neighs[node2] = min(nodeObj.neighs.get(node2,9999),cost+cost2)
        for node in self.neighs.keys():
            Node.nodes[node].neighs.pop(self.name)
        Node.nodes.pop(self.name)
    def AddTransitivity():
        for node in Node.nodes.values():
            stack = list(node.neighs.items())
            while len(stack) > 0:
                b,cost = stack.pop()
                for c,cost2 in Node.nodes[b].neighs.items():
                    if c!=node.name:
                        if node.neighs.get(c,9999) > cost+cost2:
                            node.neighs[c] = cost+cost2
                            stack.append((c,cost+cost2))



for l in open("inputs/input16.txt"):
    l = l.split(' ')
    name = l[1]
    flow = int(l[4][5:-1])
    neighs = [x[:-1] for x in l[9:]]
    Node(flow,name,neighs)
for key,item in Node.nodes.items():
    print(f"{key}: Node {item.name} with flow {item.flow} and neighs {item.neighs}")
for node in list(Node.nodes.values()):
    if node.flow == 0 and node.name != 'AA':
        node.reduce()


    
print('----')
for key,item in Node.nodes.items():
    print(f"{key}: Node {item.name} with flow {item.flow} and neighs {item.neighs}")
Node.AddTransitivity()
print('----')
for key,item in Node.nodes.items():
    print(f"{key}: Node {item.name} with flow {item.flow} and neighs {item.neighs}")

# NOT FINISHED