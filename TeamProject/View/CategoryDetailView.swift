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
		VStack {
			
			//				PieChart(chart9
			Spacer()
			//				Text("id: \(budgetType.id) | Name: \(budgetType.type)")
			HStack {
				Button {
					viewModel.addView.toggle()
				} label: {
					Text("Add")
				}
				Spacer()
				Button {
					viewModel.predictView.toggle()
				} label: {
					Text("Predicate")
				}
			}
			.padding([.leading,.trailing])
		}
		.sheet(isPresented: $viewModel.addView) {
			NavigationView {
				Text("Add")
					.toolbar {
						ToolbarItem(placement: .navigationBarLeading) {
							Button("Dissmis") {
								viewModel.addView.toggle()
							}
						}
					}
			}
		}
		.sheet(isPresented: $viewModel.predictView) {
			NavigationView {
				Text("Predict")
					.toolbar {
						ToolbarItem(placement: .navigationBarLeading) {
							Button("Dissmis") {
								viewModel.predictView.toggle()
							}
						}
					}
			}
		}
	}
}

struct CategoryDetailView_Previews: PreviewProvider {
	static var previews: some View {
		CategoryDetailView(viewModel: CategoryDetailViewModel(budgetType: BudgetType.budgetTypeMock))
	}
}
