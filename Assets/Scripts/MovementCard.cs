using System.Collections.Generic;
using UnityEngine;

public class MovementCard : Card {

    public bool DoesTp = false; //does this involve a teleport
    public bool DoesHop = true; //does this involve hops
    public int hops;

    protected virtual void Start(){
        base.Start();
        cardType = Type.MOVEMENT;
    }

    // CALL THIS TO KNOW WHERE YOU CAN MOVE TO
    // TAKES WHERE THEY ARE, AND HOW MANY EXTRA OR FEWER HOPS THEY CAN MAKE
    public HashSet<Planet> GetHopTargets(Planet from, int hopMod){
        HashSet<Planet> found = new HashSet<Planet>();
        HashSet<Planet> unexplored = new HashSet<Planet>();
        int hopsLeft = hops + hopMod;
        Debug.Log(from);
        unexplored.Add(from);
        while(hopsLeft > 0 && unexplored.Count > 0){
            HashSet<Planet> newUnexplored = new HashSet<Planet>();
            foreach(Planet p in unexplored){
                foreach(Planet q in p.connections){
                    if(!found.Contains(q) && canMoveThrough(q)){
                        newUnexplored.Add(q);
                    }
                    if(!q.isStart && canMoveTo(q)){
                        found.Add(q);
                    }
                }
            }
            unexplored = newUnexplored;
            hopsLeft--;
        }
        return found;

    }

    public virtual bool IsLegalTp(int fuel, List<Card> cardsPaid, Planet from, int hopMod, Planet to){
        return Usable(fuel, cardsPaid) && CanTpTo(from, to);
    }

    public virtual bool IsLegalHop(int fuel, List<Card> cardsPaid, Planet from, int hopMod, Planet to){
        return Usable(fuel, cardsPaid) && GetHopTargets(from, hopMod).Contains(to);
    }


    // returns true if you can TP to the give planet
    public virtual bool CanTpTo(Planet from, Planet to){
        return false;
    }

    protected virtual bool canMoveThrough(Planet p){
        return true;
    }

    protected virtual bool canMoveTo(Planet p){
        return true;
    }


}