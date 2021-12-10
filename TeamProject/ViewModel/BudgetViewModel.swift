//
//  BudgetViewModel.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 10/12/2021.
//

import Foundation

@MainActor
class BudgetViewModel: ObservableObject {
	@Published var budgetCategories: [BudgetType] = []
	@Published var isLoading = false
	private let fetcher: BudgetFetcher
	private var task: Task<(),Never>?
	
	init(fetcher: BudgetFetcher) {
		self.fetcher = fetcher
	}
	
	func getCategories() {
		task = Task {
			isLoading = true
			do {
				budgetCategories = try await fetcher.getArrayOfBudgetType()
				
			} catch let error {
				print(error.localizedDescription)
			}
			isLoading = false
		}
	}
	
	func hiddenTheSalaryAndBudget(_ id: Int) -> Bool {
		return ![10,11].contains(id)
	}
	
	
}
