## World Rules

We live in a torus (donut) woorls.

A person that has at least 50% health will move in a random direction each turn.

Aperson that is infected will infect its neighbours by the chance of the infection rate.

We implement the 8-neighborhood.

Healing Function: [deathrate, vulnerability]???

Sumilation stops automatically when no infected are available or no alive people are available

Beware of:
- Only one person per cell.
- Shuffle the order of persons to have a nice day.

## UI
Some statistics
- Amount of current infections
- Amount of people died
- FPS
- Round number
- Possibility for configuration
- Simulation Start/Cancle
- Show in Picturebox the current state of the world
- UI is only display logic in library

## Some Extra stuff for motivated students (all of you)
- [ ] Extra points: Immunity for x-iterations after infection is over.
- [ ] Extra points: Incubation time.
- [ ] Extra points: More intelligent movement.