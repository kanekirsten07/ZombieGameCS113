#pragma strict

var force : float = 20.0f;
 
function Start() {
    rigidbody2D.AddForce(new Vector3(700,400,0)); 
}