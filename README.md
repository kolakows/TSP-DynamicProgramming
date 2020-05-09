# TSP-DynamicProgramming

Implementation of Bellman dynamic algorithm for the Traveling Salesman Problem.

## TSP program

### Input files

The program accepts weighted undirected complete graphs `G = (V, E), V = {v_0, v_1, ..., v_n-1}` represented in `*.txt` files in the format:
```
n
w_01, w_02, ..., w_0n-1
w_12, w_13, ..., w_1n-1
...
w_n-2n-1 
```
where `w_ij` is the weight of edge `(v_i, v_j)`. In interactive mode only one file is processed at time.

### Output files
For each input graph the cycle found is saved in a `*.txt` file in the form:
```
c
vp_0, vp_1, ..., vp_n-1
```
where `c` is cycle cost (sum of edges weights), `vp_0, ..., vp_n-1` are graph vertices in their order of appearance on the cycle, starting from vertex `vp_0` and ending on the last cycle vertex before coming back to the starting `vp_0`. In this program `v_n-1` is always the starting vertex `vp_0`.

Additionally, results from the whole program run (all instances tested) are aggregated into a `*.csv` file that contains: instance file name, cost of the cycle found, best time and average time of calculating TSP for this instance.

### Running

After launching the program you can choose between the batch mode (`b`) and the interactive mode (`i`).

#### Batch mode

The application runs tests on all instances from the `tests/` directory and writes results under `results/`. Each instance is calculated 10 times. The results for each repetition as well as summary of instance results are logged in the console.

#### Interactive mode

You need to select an input graph file. Then you can choose result cycle file path, statistics  `*.csv` file path, the number of repetitions of the algorithm and whether the program should report memory usage. No choice (empty strings) means accepting default values: `<graph_name>_out.txt` cycle file, `results.csv` statistics file, 10 repetitions and no memory recording.

## Graph Generator helper program

The program generates random graph instances and saves them to `*.txt ` files according to the input file format for the `TSP` program. The generator parameters: graph sizes range, graph weights range, generator seed and output files location can be set in the code only. The program is not interactive, after launching and generating graphs it prints a message on the console and finishes.
