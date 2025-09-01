using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
  [SerializeField] private string startingMoney = "500";
  private IdleNumber money;
  public IdleNumber Money { get => money; private set => money = value; }

  public UnityEvent<IdleNumber> MoneyChanged = new();

  public void Purchase(IdleNumber itemValue)
  {
    if (!Money.Compare(itemValue)) return;

    Money.Subtract(itemValue);

    MoneyChanged.Invoke(Money);
  }

  public void Earn(IdleNumber earnAmount)
  {
    Money.Add(earnAmount);

    MoneyChanged.Invoke(Money);
  }
}
