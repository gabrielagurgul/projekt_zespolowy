//
//  CategoryView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 05/12/2021.
//

import SwiftUI

struct CategoryView: View {
    let budgetType: BudgetType
    var body: some View {
        
        Text(budgetType.type)
    }
}

struct CategoryView_Previews: PreviewProvider {
    static var previews: some View {
        CategoryView(budgetType: BudgetType.budgetTypeMock)
    }
}
