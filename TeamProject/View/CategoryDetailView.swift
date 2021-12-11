//
//  CategoryDetailView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 10/12/2021.
//

import SwiftUI

struct CategoryDetailView: View {
	@EnvironmentObject var viewModel: BudgetViewModel
	let budgetType: BudgetType
    var body: some View {
		VStack {
			Text("id: \(budgetType.id) | Name: \(budgetType.type)")
		}
		.font(.largeTitle)
    }
}

struct CategoryDetailView_Previews: PreviewProvider {
    static var previews: some View {
		CategoryDetailView(budgetType: BudgetType.budgetTypeMock)
    }
}
