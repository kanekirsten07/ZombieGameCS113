#pragma strict

function OnMouseDown(){
//    Application.LoadLevel("Survival");
    Application.LoadLevel(Random.Range(2, Application.levelCount));
}

/*
function Start () {

}

function Update () {

}
*/