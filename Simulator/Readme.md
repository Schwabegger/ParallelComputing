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


Welt, Rechteck, Breite und Höhe konfig
Menschen in Welt, je Mensch 1 Pixelquadrat, pro Pixelquadrat max 1 Mensch
Initiale Anzahl an Personen leben in Welt konfig
Initiale Anzahl davon infizierter Personen (DmgDelay and Incubation Time apply to initial infected as well)
Jeden Morgen triggert Heal->Move->Infect->Damage
Heal:
	- Menschen werden Gesund nach Krankheitsdauer
	- Menschen die Gesund werden bekommen zusätzliche Schadensresistenz (Defence)
	- Menschen die am Leben sind regenerieren Leben
	- Infizierte Menschen heilen in % des aktuellen Lebens
	- Gesunde Menschen heilen in % des maximalen Lebens
Move:
	- Menschen bewegen sich nacheinander
	- Reihenfolge wird zufällig geshuffelt
	- Bewegung über Rand hinaus möglich
	- Bewegung in 8 Richtungen möglich
	- Nur Menschen mit >50% Leben bewegen sich
	- Menschen bleiben nicht stehen, außer es ist kein angrenzendes Feld frei
Infect:
	- Kann nur angrenzende Personen anstecken
	- Kann in 8 Richtungen anstecken
	- Kann andere erst nach Inkubationszeit anstecken
	- Kann nur dann angesteckt werden wenn nicht in Immunitätszeit
	- Ansteckung nach Wahrscheinlichkeit (Ansteckungsrate)
	- Keine Ansteckungsresistenzen
	- Infizierte Menschen treffen sich -> keine Auswirkung
	- Ansteckungsrate ist mit Pool fixiert (Weltweit)
Damage:
	- Infizierte Personen bekommen Schaden
	- Schaden tritt erst nach Verzögerung ein
	- Schaden in % des maximalen Lebens
	- Menschen haben individuelle Schadensreduktion (Defence)
	- Schadensreduktion erhöht sich um 0,5% pro Tag der Krankheitsdauer (Defence + 0,5% * Tage)
	- Fällt das Leben der Person auf 0 stirbt diese und wird entfernt
End of day:
	- Zeiten werden um einen Tag reduziert


Virus:
	- Damage in % Max Health
	- Ansteckungsrate
	- (Fatality Rate)


Anderes:
Inkubationszeit nach Gaußscher Glockenkurve
Dmg Delay nach Gaußscher Glockenkurve
Optional: Dmg Crit Pool, Weltweit



Move
Auto Healing
Dmg
Dmg Factor
Infection Rate (actual infection rate)
Incubation Time
Dmg Delay
Number of initially infected persons