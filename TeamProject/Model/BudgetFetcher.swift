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
	func getArrayOfBudgetCategories() async throws ->  [BudgetType]
	func getArrayOfBudgetsOf(type: ID) async throws -> [Budget]
	func getBudgetBy(_ id: ID) async throws -> Budget
	func getUserBudget() async throws -> PLN
	func getUserExpensives() async throws -> [PLN]
	func getPredictionforType(_ id: ID, budget: Budget) async throws -> Double
}

class BudgetFetcherImpl: BudgetFetcher {
	private let session: URLSession
	
	init() {
		self.session = URLSession(configuration: .ephemeral)
	}
	
	func getArrayOfBudgetCategories() async throws -> [BudgetType] {
		let urlRequest = URLRequest(url: API.GET.budgetTypeDescription)
		let (data,_) = try await session.data(for: urlRequest)
		guard let arrayOfBudgetsCategories = try? JSONDecoder().decode([BudgetType].self, from: data) else {
			throw ApiError.InvalidDataDecoding
		}
		return arrayOfBudgetsCategories
	}
	
	func getArrayOfBudgetsOf(type: ID) async throws -> [Budget] {
		let urlRequest = URLRequest(url: API.GET.budgetDescription)
		let (data,_) = try await session.data(for: urlRequest)
		guard let arrayOfBudgets = try? JSONDecoder().decode([Budget].self, from: data) else {
			throw ApiError.InvalidDataDecoding
		}
		print(arrayOfBudgets)
		return arrayOfBudgets
	}
	
	func getBudgetBy(_ id: ID) async throws -> Budget {
		return Budget.budgetMock
	}
	
	func getUserBudget() async throws -> PLN {
		return 10.0
	}
	
	func getUserExpensives() async throws -> [PLN] {
		[-10.0, -5.1, -3.2]
	}
	
	func getPredictionforType(_ id: ID, budget: Budget) async throws -> Double {
		return 0.8
	}
	
	
}

extension BudgetFetcherImpl {
	enum REST: String {
		case GET = "GET"
		case PUT = "PUT"
		case DELETE = "DELETE"
		case POST = "POST"
	}
	
	enum ApiError: Error, LocalizedError {
		case InvalidDataDecoding
		
		var errorDescription: String? {
			switch self {
			case .InvalidDataDecoding:
				return "Nie udało się przekonwertować danych otrzymanych od serwera"
			}
		}
	}
}
