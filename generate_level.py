import math

def generate():
    top = 160
    bottom = 330
    base = 500

    length = 30000

    units_per_segment = 50

    points = []

    range_size = bottom - top
    for x in range(0,length,units_per_segment):
        points.append([x,top + ((math.sin(x) + 1)/2) * range_size])



    print(max([p[1] for p in points]))
    print(min([p[1] for p in points]))

    first = points[0]
    last = points[-1]

    points.append([last[0], base])
    points.append([first[0], base])

    
    print('polygon = PoolVector2Array( ', end='')
    for point in points[:-1]:
        print(point[0], end=', ')
        print(point[1], end=', ')

    print(points[-1][0], end=', ')
    print(points[-1][1], end=' ')
    print(')')


generate()
