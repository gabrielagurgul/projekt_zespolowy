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
	@Published var showAlert: Bool = false
	@Published var error: Error?
	@Published var data = Budget.BudgetAPI()
	@Published var chartData: PieChartData = PieChartData(dataSets: PieDataSet(dataPoints: [], legendTitle: ""))
	var id: ID {
		get {
			budgetType.id
		}
	}
	
	init (budgetType: BudgetType) {
		self.budgetType = budgetType
	}
	
	func getBudgets() {
		Task {
			isLoading = true
			do {
				budgets = try await getBudgetBy(budgetType.id).map { Budget.createBudget(from: $0)}
				chartData = PieChartData(dataSets: PieDataSet(dataPoints: createChartFromBudget(), legendTitle: "wtf"))
			} catch let myError {
				error = myError
				showAlert = true
			}
			isLoading = false
		}
	}
	
	func add() {
		Task {
			isLoading = true
			do {
				_ = try await addBudget(Budget.createBudget(from: data), category: REST.getCategoryById(id: budgetType.id))
			} catch let myError {
				error = myError
				showAlert = true
			}
			isLoading = false
			getBudgets()
		}
		
	}
	
	private func createChartFromBudget() -> [PieChartDataPoint] {
		return budgets.compactMap { $0.createPieData()}
	}
	
}
