using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IdleNumber
{
  private List<int> places = new();

  public IdleNumber(string startingValue = "0")
  {
    places = ToArray(startingValue);
  }

  public List<int> GetPlaces()
  {
    return places;
  }

  public void SetPlaces(List<int> newPlaces)
  {
    places = newPlaces;
  }

  public static List<int> ToArray(string num)
  {
    int placesToAdd = Mathf.CeilToInt(num.Length / 3.0f);
    int[] newPlaces = new int[placesToAdd];
    for (int i = 0; i < placesToAdd; i++)
    {
      int substringIndex = num.Length - (i + 1) * 3;
      int substringLength = substringIndex > 0 ? 3 : 3 + substringIndex;
      int numToAdd = int.Parse(num.Substring(Mathf.Max(substringIndex, 0), substringLength));
      newPlaces[i] = numToAdd;
    }

    return new(newPlaces);
  }

  public string ToNum()
  {
    StringBuilder sb = new();

    int lastPlaceIndex = places.Count - 1;
    for (int i = lastPlaceIndex; i >= 0; i--)
    {
      string strToAdd = i == lastPlaceIndex ? places[i].ToString() : $"{places[i]}".PadLeft(3, '0');
      sb.Append(strToAdd);
    }

    return sb.ToString();
  }

  public void Add(IdleNumber numberToAdd)
  {
    List<int> addNumberPlaces = numberToAdd.GetPlaces();

    int placesCount = places.Count;
    int addNumberCount = addNumberPlaces.Count;

    int carry = 0;
    int i = 0;
    while (i < addNumberCount || carry != 0)
    {
      int currentAddValue = 0;
      if (i < placesCount) currentAddValue += places[i];
      if (i < addNumberCount) currentAddValue += addNumberPlaces[i];

      currentAddValue += carry;

      carry = currentAddValue / 1000;
      currentAddValue %= 1000;

      if (i < placesCount)
        places[i] = currentAddValue;
      else
        places.Add(currentAddValue);

      i++;
    }
  }

  public void Subtract(IdleNumber numberToSubtract)
  {
    List<int> subNumberPlaces = numberToSubtract.GetPlaces();

    int placesCount = places.Count;
    int subNumberCount = subNumberPlaces.Count;

    if (subNumberCount > placesCount) return;

    int borrow = 0;
    int i = 0;
    while (i < placesCount)
    {
      int currentSubValue = places[i];
      if (i < subNumberCount) currentSubValue -= subNumberPlaces[i];

      currentSubValue -= borrow;

      if (currentSubValue < 0)
      {
        currentSubValue += 1000;
        borrow = 1;
      }
      else
      {
        borrow = 0;
      }

      places[i] = currentSubValue;

      i++;
    }

    int largestPlaceWithoutZero = placesCount - 1;
    while (places[largestPlaceWithoutZero] == 0 && largestPlaceWithoutZero > 0)
    {
      largestPlaceWithoutZero--;
    }

    places = places.GetRange(0, largestPlaceWithoutZero + 1);
  }

  public void Multiply(float multiplier)
  {
    int placesCount = places.Count;

    int carry = 0;
    int i = 0;
    while (i < placesCount || carry != 0)
    {
      int currentAddValue = 0;
      if (i < placesCount)
      {
        currentAddValue += places[i];
        currentAddValue = (int)(currentAddValue * multiplier);
      }

      currentAddValue += carry;

      carry = currentAddValue / 1000;
      currentAddValue %= 1000;

      if (i < placesCount)
        places[i] = currentAddValue;
      else
        places.Add(currentAddValue);

      i++;
    }
  }

  public bool Compare(IdleNumber numberToCompare)
  {
    List<int> comparePlaces = numberToCompare.GetPlaces();

    if (comparePlaces.Count > places.Count) return false;
    if (places.Count > comparePlaces.Count) return true;

    for (int i = comparePlaces.Count - 1; i >= 0; i--)
    {
      if (comparePlaces[i] > places[i]) return false;
      if (places[i] > comparePlaces[i]) return true;
    }

    return true;
  }

  public string GetDisplay(int significantFigures)
  {
    StringBuilder sb = new();

    string suffix = Enum.GetName(typeof(NumberPrefixes), places.Count);

    int lastIndex = places.Count - 1;
    sb.Append(places[lastIndex]);

    int placesRemaining = significantFigures - sb.Length;

    if (placesRemaining <= 0 || lastIndex == 0)
    {
      sb.Append(suffix);
      return sb.ToString();
    }

    sb.Append(".");

    for (int i = lastIndex - 1; i >= 0 && placesRemaining >= 0; i--)
    {
      string toAppend = places[i].ToString().PadLeft('0');
      toAppend = toAppend[..Mathf.Min(placesRemaining, toAppend.Length)];

      sb.Append(toAppend);

      placesRemaining -= toAppend.Length;
    }

    sb.Append(suffix);

    return sb.ToString();
  }
}
