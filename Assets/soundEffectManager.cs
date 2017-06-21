using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectManager : MonoBehaviour
{
    public static soundEffectManager staticSoundEffect;
    AudioSource myAudio;

    public List<AudioClip> characterAttack;
    public List<AudioClip> characterMove;
    public AudioClip characterCantMove;
    public AudioClip characterHurtWithHungry;
    public AudioClip characterHurtWithThorn;
    public AudioClip characterJump;
    public AudioClip characterOpenChest;

    public AudioClip get_fruit;
    public AudioClip get_item;

    public AudioClip monster_move;
    public AudioClip monster1_dead;
    public AudioClip monster2_dead;
    public AudioClip monster3_dead;
    public AudioClip monster4_attack;
    public AudioClip monster4_dead;
    
    public AudioClip button_onItemUse;
    public AudioClip Click_Button;
    public AudioClip gameOver;
    public AudioClip levelPass;
    public AudioClip TapToStart;

    public AudioClip ability_UP;
    public AudioClip crystal_light;
    public AudioClip equitment_shield_up;
    public AudioClip equitment_sword_up;

    void Awake()
    {
        myAudio = GetComponent<AudioSource>();
        if (staticSoundEffect != null)
        {
            Destroy(gameObject);
        }
        else
        {
            staticSoundEffect = this;
        }
        DontDestroyOnLoad(gameObject);

    }

    public void play_characterAttack(int number)
    {
        myAudio.PlayOneShot(characterAttack[number], 1.0f);
    }

    public void play_characterMove(int number)
    {
        myAudio.PlayOneShot(characterMove[number], 1.0f);
    }

    public void play_characterCantMove()
    {
        myAudio.PlayOneShot(characterCantMove, 1.0f);
    }

    public void play_characterHurtWithHungry()
    {
        myAudio.PlayOneShot(characterHurtWithHungry, 1.0f);
    }
    public void play_characterHurtWithThorn()
    {
        myAudio.PlayOneShot(characterHurtWithThorn, 1.0f);
    }
    public void play_characterJump()
    {
        myAudio.PlayOneShot(characterJump, 1.0f);
    }
    public void play_characterOpenChest()
    {
        myAudio.PlayOneShot(characterOpenChest, 6.5f);
    }
    public void play_get_fruit()
    {
        myAudio.PlayOneShot(get_fruit, 1.0f);
    }
    public void play_get_item()
    {
        myAudio.PlayOneShot(get_item, 1.0f);
    }

    public void play_monster_move()
    {
        myAudio.PlayOneShot(monster_move, 1.0f);
    }
    public void play_monster1_dead()
    {
        myAudio.PlayOneShot(monster1_dead, 1.0f);
    }
    public void play_monster2_dead()
    {
        myAudio.PlayOneShot(monster2_dead, 1.0f);
    }
    public void play_monster3_dead()
    {
        myAudio.PlayOneShot(monster3_dead, 1.0f);
    }
    public void play_monster4_attack()
    {
        myAudio.PlayOneShot(monster4_attack, 1.0f);
    }
    public void play_monster4_dead()
    {
        myAudio.PlayOneShot(monster4_dead, 1.0f);
    }
    public void play_button_onItemUse()
    {
        myAudio.PlayOneShot(button_onItemUse, 4.0f);
    }
    public void play_Click_Button()
    {
        myAudio.PlayOneShot(Click_Button, 1.0f);
    }
    public void play_gameOver()
    {
        myAudio.PlayOneShot(gameOver, 8.0f);
    }
    public void play_levelPass()
    {
        myAudio.PlayOneShot(levelPass, 1.0f);
    }
    public void play_TapToStart()
    {
        myAudio.PlayOneShot(TapToStart, 1.0f);
    }
    public void play_ability_UP()
    {
        myAudio.PlayOneShot(ability_UP, 1.0f);
    }
    public void play_crystal_light()
    {
        myAudio.PlayOneShot(crystal_light, 1.0f);
    }
    public void play_equitment_shield_up()
    {
        myAudio.PlayOneShot(equitment_shield_up, 1.0f);
    }
    public void play_equitment_sword_up()
    {
        myAudio.PlayOneShot(equitment_sword_up, 1.0f);
    }


}
