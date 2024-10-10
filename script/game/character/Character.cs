using System.Collections;
using UnityEngine;

namespace Assets.script.game.character {
    public class Character :MonoBehaviour{


        public Rigidbody rb;
        public float speed=1;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        public void jump() {
           
        }

        public void move() {

        }

        private void FixedUpdate() {
            float horizontal = Input.GetAxis("Horizontal");
            float vetical = Input.GetAxis("Vertical");

   
            rb.velocity = new Vector3(horizontal * speed * Time.fixedDeltaTime, rb.velocity.z, vetical * speed * Time.fixedDeltaTime);

        }

    }
}