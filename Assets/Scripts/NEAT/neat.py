import neat
import os

class Surfer:
    '''Surfer class'''


def fitness_function(genomes, config):
    nets = []
    ge = []
    surfers = []

    for g in genomes:
        g.fitness = 0
        net = neat.nn.FeedForwardNetwork(g, config)
        nets.append(net)
        surfers.append(Surfer())
        ge.append(g)
        



def run(config_path):
    config = neat.config.Config(neat.DefaultGenome, neat.DefualtReproduction,
                    neat.DefualtSpeciesSet, neat.DefualtStagnation,
                    config_path)
    
    p = neat.Population(config)
    p.add_reporter(neat.StdOutReporter(True))
    stats = neat.StatisticsReporter()
    p.add_reporter(stats)

    winner = p.run(fitness_function,50)


local_dir = os.path.dirname(__file__)
config_path = os.path.join(local_dir, "config-feedforward.txt")
run(config_path)