using IM.GameData;
using UnityEngine;
using UnityEngine.UI;

namespace IM.UI.Game
{
    public class RespawnPopup : MonoBehaviour
    {
        [SerializeField] private GameObject showAdsButtonObject;
        [SerializeField] private Button showAdsButton;
        [SerializeField] private Button surrenderButton;

        private RespawnPopupPresenter _presenter;

        private void Awake()
        {
            _presenter = new RespawnPopupPresenter(this);
        }

        private void OnEnable()
        {
            showAdsButtonObject.SetActive(GameStats.Instance.CanRespawn);
            showAdsButton.onClick.AddListener(_presenter.OnShowAdsPressed);
            surrenderButton.onClick.AddListener(_presenter.OnSurrenderPressed);
        }

        public void OnDisable()
        {
            showAdsButton.onClick.RemoveListener(_presenter.OnShowAdsPressed);
            surrenderButton.onClick.RemoveListener(_presenter.OnSurrenderPressed);
        }
    }
}