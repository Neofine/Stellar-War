using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OverallUtility {
    public static string simplify(string name) {
        string answer = "";
        string fullName = name;
        int idx = 0;
        while ((fullName[idx] >= 'a' && fullName[idx] <= 'z') || (fullName[idx] >= 'A' && fullName[idx] <= 'Z'))
            idx++;

        for (int i = 0; i < idx; i++) {
            answer += fullName[i];
        }

        return answer;
    }
}
