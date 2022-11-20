import math

def generate():
    top = 160
    bottom = 550
    base = 500000

    length =  30000 * 10

    units_per_segment = 50

    points = []

    hill_width = 500

    range_size = bottom - top
    for x in range(0,length,units_per_segment):
        y = math.sin(x*math.pi/500)
        y += math.sin(x*math.pi/1000)
        y += math.sin(x*math.pi/3000)
        points.append([x,y])

    ymax = (max([p[1] for p in points]))
    ymin = (min([p[1] for p in points]))

    y_cur_range = ymax - ymin

    for p in points:
        p[1] = top + ((p[1] - ymin) / (ymax - ymin)) * (bottom - top)


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
