//
//  CategoryDetailViewModel.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 20/12/2021.
//

import Foundation
import SwiftUICharts

@MainActor
class CategoryDetailViewModel: ObservableObject {
	private let budgetType: BudgetType
	@Published var addView: Bool = false
	@Published var predictView: Bool = false
	
//	var chartb
	
	
	init (budgetType: BudgetType) {
		self.budgetType = budgetType
	}
	
	func addBudget() {
		
	}
}
