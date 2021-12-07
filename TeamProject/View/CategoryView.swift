//
//  CategoryView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 05/12/2021.
//

import SwiftUI

struct CategoryView: View {
    let budgetType: String
    var body: some View {
        Text(budgetType)
    }
}

struct CategoryView_Previews: PreviewProvider {
    static var previews: some View {
        CategoryView(budgetType: "Makaron")
    }
}
