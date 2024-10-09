using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickySettings : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
    [SerializeField] private AudioClip _compressClip, _uncompressClip;
    [SerializeField] private AudioSource _source;


    public void OnPointerDown(PointerEventData eventData)
    {
        
        _source.PlayOneShot(_compressClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      
        _source.PlayOneShot(_uncompressClip);
        
    }

   
  


}
