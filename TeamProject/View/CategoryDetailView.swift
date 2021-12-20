//
//  CategoryDetailView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 10/12/2021.
//

import SwiftUI
import SwiftUICharts

struct CategoryDetailView: View {
	@ObservedObject var viewModel: CategoryDetailViewModel
	var body: some View {
		GeometryReader { proxy in
			VStack {
//				PieChart(chart9
				Spacer()
//				Text("id: \(budgetType.id) | Name: \(budgetType.type)")
				HStack {
					Button {} label: {
						Text("Add")
					}
					Spacer()
					Button {} label: {
						Text("Predicate")
					}
				}
				.padding([.leading,.trailing])
			}
		}
//		.background{Image("p2")}
	}
}

struct CategoryDetailView_Previews: PreviewProvider {
	static var previews: some View {
		CategoryDetailView(viewModel: CategoryDetailViewModel(budgetType: BudgetType.budgetTypeMock))
	}
}
