// A CLASS TO HANDLE REVEALING CARDS AT PLANETS AS PART OF CARD ABILITIES

public class PlanetScouterAbility {

    private int planetsEffected;
    private int revealedPerPlannet;
    public PlanetScouterAbility(int planetsEffected, int revealedPerPlannet){
        this.planetsEffected = planetsEffected;
        this.revealedPerPlannet = revealedPerPlannet;
    }

    public void ScoutFrom(Planet p){
        int planetsScouted = 0;
        // this is a very deterministic way of going about what should really be random
        foreach(Planet neighbor in p.connections){
            neighbor.RevealCards(revealedPerPlannet);
            planetsScouted++;
            if(planetsScouted >= planetsEffected){
                break;
            }
        }
    }

}