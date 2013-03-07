Feature: SituacionActual
	In order to track my day
	As a worker
	I want to be able to start and stop my day

Scenario: Begin day
	Given I'm an authenticated as prueba with password prueba
	And there is not a day started
	When I press the start day button
	Then the stop day button is showe
	And the start pause button is showed
