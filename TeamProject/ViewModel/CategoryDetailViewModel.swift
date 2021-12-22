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
	@Published var budgets: [Budget] = []
	@Published var isLoading: Bool = false
	
//	var chartb
	
	
	init (budgetType: BudgetType) {
		self.budgetType = budgetType
	}
	
	func getBudgets() {
		Task {
			isLoading = true
			do {
				budgets = try await getBudgetBy(budgetType.id).map { Budget.createBudget(from: $0)}
				print(budgets)
			} catch let error {
				print(error.localizedDescription)
			}
			isLoading = false
		}
	}
	
	func addBudget() {
		
	}
}
