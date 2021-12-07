//
//  ContentView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 05/12/2021.
//

import SwiftUI

struct ContentView: View {
    var body: some View {
        ScrollView {
            ForEach(BudgetType.arrayOfBudgetTypes) { budgetType in
                CategoryView(budgetType: budgetType)
            }
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
