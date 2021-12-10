//
//  BudgetFetcher.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 10/12/2021.
//

import Foundation

protocol BudgetFetcher {
	typealias ID = Int
	typealias PLN = Double
	func getArrayOfBudgetType() async throws ->  [BudgetType]
	func getArrayOfBudgetsOf(type: ID) async throws -> [Budget]
	func getBudgetBy(_ id: ID) async throws -> Budget
	func getUserBudget() async throws -> PLN
	func getUserExpensives() async throws -> [PLN]
	func getPredictionforType(_ id: ID, budget: Budget) async throws -> Double
}
