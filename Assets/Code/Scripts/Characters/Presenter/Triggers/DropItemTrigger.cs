using IntoTheWilds.Inventory;
using IntoTheWilds.Quest;
using UnityEngine;
using VContainer;

public class DropItemTrigger : MonoBehaviour
{
    [SerializeField] private GameEventQuestProgress _questProgressEvent;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _pickupSounds;
    [SerializeField, Range(0f, 1f)] protected float _volumeClips = 1f;

    private PlayerInventory _inventory;

    private DropItemInstance _dropItemInstance;
    private bool _isStayOnItem;

    [Inject]
    public void Constuct(PlayerInventory inventory)
    {
        _inventory = inventory;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DropItems"))
        {
            _dropItemInstance = collision.gameObject.GetComponent<DropItemInstance>();

            if (_dropItemInstance.TryGetItem(out ItemSlot Item) == true)
            {
                TakeItem(Item);
            } 
            else
            {
                _isStayOnItem = true;
            }
        }
    }

    private void Update()
    {
        if (_isStayOnItem)
        {
            if (_dropItemInstance.TryGetItem(out ItemSlot Item) == true)
            {
                TakeItem(Item);
            }
        }
    }

    private void TakeItem(ItemSlot Item)
    {
        if (_inventory.IsAndTakeItem(Item) == true)
        {
            PlayShotAudio();

            _questProgressEvent.TriggerEvent(
                new QuestProgressData
                {
                    ObjectiveType = ObjectiveType.Collect,
                    ResourceType = _dropItemInstance.GetResourceTypes(),
                    Amount = _dropItemInstance.GetAmount(),
                });

            _dropItemInstance.DestroyItem();

            _dropItemInstance = null;
            _isStayOnItem = false;
        }
    }
    private void PlayShotAudio()
    {
        _audioSource.PlayOneShot(RandomClip(_pickupSounds), _volumeClips);
    }

    private AudioClip RandomClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}
