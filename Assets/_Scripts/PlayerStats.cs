using UnityEngine;
using TMPro;

public sealed class PlayerStats : MonoBehaviour
{
    float _attackDamage;
    int attackDamageLevel;
    public float AttackDamage
    {
        get
        {
            return _attackDamage;
        }
        set
        {
            if (attackDamageLevel == 3)
            {
                print("Max attack damage level!");
                return;
            }

            // if (!TryToPayUp(attackDamageLevel))
            // {
            //     print("Not enough dough!");
            //     return;
            // }

            _attackDamage += value;
            attackDamageLevel++;
            damageValueText.text = _attackDamage.ToString();
        }
    }

    float _attackSpeed;
    int attackSpeedLevel;
    public float AttackSpeed
    {
        get
        {
            return _attackSpeed;
        }
        set
        {

            if (attackSpeedLevel == 4)
            {
                print("Max attack speed level!");
                return;
            }
            // if (!TryToPayUp(attackSpeedLevel))
            // {
            //     print("Not enough dough!");
            //     return;
            // }

            _attackSpeed += value;
            attackSpeedLevel++;
            attackSpeedValueText.text = _attackSpeed.ToString();
        }
    }

    float _attackRange;
    int attackRangeLevel;
    public float AttackRange
    {
        get
        {
            return _attackRange;
        }
        set
        {
            if (attackRangeLevel == 3)
            {
                print("Max attack range level!");
                return;
            }

            // if (!TryToPayUp(attackRangeLevel))
            // {
            //     print("Not enough dough!");
            //     return;
            // }

            _attackRange += value;
            attackRangeLevel++;
            attackRangeValueText.text = _attackRange.ToString();
        }
    }

    // int cashMoney;

    // void GetPaid(int dough)
    // {
    //     cashMoney += dough;
    //     moneyCounter.text = $"${cashMoney}";
    // }

    // public bool TryToPayUp(int amount)
    // {
    //     if (amount <= cashMoney)
    //     {
    //         cashMoney -= amount;
    //         moneyCounter.text = $"${cashMoney}";
    //         return true;
    //     }
    //     return false;
    // }

    public TMP_Text damageValueText;
    public TMP_Text attackSpeedValueText;
    public TMP_Text attackRangeValueText;

    void Awake()
    {
        ResetStats();
    }

    public void ResetStats()
    {
        _attackDamage = 0;
        attackDamageLevel = 0;
        AttackDamage++;

        _attackSpeed = 0;
        attackSpeedLevel = 0;
        AttackSpeed++;

        _attackRange = 0;
        attackRangeLevel = 0;
        AttackRange = 5;
    }
}